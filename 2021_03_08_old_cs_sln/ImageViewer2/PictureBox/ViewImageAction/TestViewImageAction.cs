using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ErrorLog;
using ImageViewer2;
using FileList;
using System.Diagnostics;
using System.Reflection;
using ViewImageAction.BaseForm;
using ViewImageAction.Events;
using System.IO;

namespace ViewImageAction
{
    class TestViewImageAction
    {
        readonly ErrorLog.IErrorLog _errorLog;
        PictureBox _picturebox;
        Panel _panel;
        readonly ViewImage _viewImage;
        readonly PictureBoxControl _pictureBoxControl;
        IViewInnerControl _viewInnerControl;
        ViewControl.IViewFrameControl _viewFrameControl;
        ControlEvents _panelEvents;
        ControlEvents _pictureboxEvents;
        private readonly FileList.FileList _fileListRead;
        readonly FileList.FileListForUse _fileList;
        ViewImageControlFunction _viewImageControlFunction;
        ViewImageBasicFunction _viewImageBasicFunction;
        Function.MainFormFunction _mainformFunction;
        MoveControlEvents _moveControlEvent;
        ViewControl.FrameControlKeyEvents FrameControlKeyEvents;
        Events.BaseFormEvents _baseFormEvents;
        Events.FrameControlEvents _frameControlEvents;
        BaseForm.ContentsContorl _contentsControl;
        MainFormState _mainFormState;
        ViewImageObjects _viewImageObjects;
        ViewImageManager _viewImageManager;
        FrameControlAddEvents _frameControlAddEvents;
        InnerControlSizeLocationEvents _innerControlSizeLocationEvents;

        ViewImageAction.BaseForm.MainFormManager _mainFormManager;
        public ViewImageAction.Function.CommonFunctions Functions;
        public ViewImageAction.Settings.ImageViewerSettings Settings;
        public TestViewImageAction()
        {
            // Panel -> Panel -> PictureBox -> Image
            // BasePanel -> InnerPanel -> PictureBox -> Image
            // BaseControl -> InnerControl -> ImageControl -> ViewImage
            // innerControl = 今のPictureBox
            // IViewInnerControl

            _errorLog = GlobalErrloLog.ErrorLog;
            _pictureBoxControl = new PictureBoxControl();
            _pictureBoxControl.setErrorLog(_errorLog);
            _viewImage = new ViewImage();
            _viewImage.setErrorLog(_errorLog);
            _fileListRead = new FileList.FileList();
            _fileListRead.setErrorLog(_errorLog);
            _fileList = new FileListForUse();
            _fileList.setErrorLog(_errorLog);
            //_viewImageControlFunction = new ViewImageControlFunction();
            //_viewImageControlFunction.setErrorLog(_errorLog);
            _mainFormState = new MainFormState();
        }

        public void ViewNext(Object sender, int s) { Functions.BasicFunction.ViewNext();}
        public void ViewPrevious(Object sender, int s) { Functions.BasicFunction.ViewPrevious(); }

        public void MouseWheelEvent(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta > 0)
                {
                    // 上回転したとき
                    // 拡大 1.05倍
                    _viewImageControlFunction.ChangeSizeViewControl(ViewImageConstants.EXPANTION_RAITO);
                }
                else if (e.Delta < 0)
                {
                    // 下回転したとき
                    // 縮小 0.952倍
                    _viewImageControlFunction.ChangeSizeViewControl(ViewImageConstants.SHRINK_RAITO);
                }
            } catch( Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " MouseWheelEvent");
            }
        }

        public void DragDrop(Object sender, int s)
        {
            _fileList.FileList = _fileListRead.getList();
        }

        public ViewImageObjects getFirstViewImageObject() { return _viewImageObjects; }
        // ================================================================

        public void ViewImageAfterSetPath(ViewImageObjects viewImageObjects, List<string> list)
        {
            _viewImageBasicFunction.ViewImageAfterSetPath(viewImageObjects,list);
        }
        // Expansion,Shrink
        public void ChangeSizeViewControlChild()
        {

        }

        public void ViewImageNowIndexB(ViewImageObjects viewImageObjects)
        {
            _viewImageBasicFunction.ViewImageDefault(viewImageObjects);
            if (_errorLog.haveError())
            {
                _errorLog.ShowErrorMessageAndClearError();
            }
        }

        public void ViewImageNowIndex()
        {
            _viewImageBasicFunction.ViewImageNowIndex();
        }

        private string get_path_test_image_file(){
            string filePath = @"C:\ZMyFolder_2\default_file_path.txt";
            try
            {
                // ファイルを読み込んでその内容を表示する
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                        return line;
                    }
                }
                return "";
            }
            catch (Exception e)
            {
                // エラーが発生した場合はエラーメッセージを表示する
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return "";
        }


        public void TestImageView(Form form,Panel contentsPanel,Panel FramePanel,Panel Innerpanel, PictureBox picturebox)
        {
            // パスをセット
            //string path = get_path_test_image_file();
            //path = @"J:\ZMyFolder_2\jpgbest\gif_png_bmp\gif\160501001.gif";
            int Ret;
            Ret = Initialize((Form)form, contentsPanel, FramePanel, Innerpanel, picturebox);
            if (Ret < 1)
            {
                _errorLog.addErrorNotException(this.ToString() + "testImageView : initialize");
            }

        }

        // イメージを表示
        public int ViewImage(string path)
        {
            try
            {
                int ret = 0;
                // イメージ取得
                ret = _viewImage.setPath(path);
                if (ret < 1)
                {
                    _errorLog.addErrorNotException(this.ToString() + "setPath");
                    return -2;
                }
                // イメージをセット
                _pictureBoxControl.setImageWithDispose(_viewImage.getImage());

                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " ViewImage");
                return 0;
            }
        }

        // ================================================================
        // 初期化
        // ================================================================
        public int Initialize(
            Form baseForm,Control contents,Control parentControl,Control innerControl, Control paintControl)
        {
            try
            {
                Debug.WriteLine("Initialize");
                Debug.WriteLine(baseForm.Size.Width + " , " + baseForm.Size.Height);
                Debug.WriteLine(baseForm.ClientSize.Width +" , " + baseForm.ClientSize.Height);
                Debug.WriteLine(SystemInformation.CaptionHeight);

                paintControl.Parent = innerControl;
                innerControl.Parent = parentControl;
                parentControl.Parent = contents;
                contents.Parent = baseForm;
                //innerControl.KeyDown += parentControl.KeyDown;
                //
                int ret = 0;
                Settings = new Settings.ImageViewerSettings();
                _mainFormState = new MainFormState();

                Functions = new Function.CommonFunctions
                {
                    BasicFunction = _viewImageBasicFunction,
                    ControlFunction = _viewImageControlFunction,
                    MainFormFunction = _mainformFunction
                };
                // ======== MainForm 設定 ========
                _mainFormManager = new BaseForm.MainFormManager(baseForm);
                _mainFormManager.setErrorLog(_errorLog);
                _mainFormManager.Settings = Settings;
                _mainFormManager.Functions = Functions;
                _mainFormManager.State = _mainFormState;
                
                ret = _mainFormManager.initialize();
                if (ret < 1)
                { _errorLog.addErrorNotException(this.ToString(),"_mainFormManager initialize failed"); }
                if (_errorLog.haveError())
                { _errorLog.ShowErrorMessageAndClearError(); }
                // ======== Contents Panel 設定 ========
                _contentsControl = new BaseForm.ContentsContorl((Panel)contents);
                _contentsControl.setErrorLog(_errorLog);
                _contentsControl.setParentControl(baseForm);

                // フォームの大きさに追随して伸縮するようになる
                contents.Dock = DockStyle.Fill;

                // アンカー  左上に表示
                contents.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                // ======== PictureBox Control 設定 ========
                // PictureBox set
                if (paintControl is PictureBox box) { _picturebox = box; }
                if (_picturebox == null)
                { _errorLog.addErrorNotException(this.ToString() + "initialize Failed"); return -1; }
                // Panel Set
                if (parentControl is Panel panel) { _panel = panel; }
                if (parentControl == null)
                { _errorLog.addErrorNotException(ToString() + "initialize Failed"); return -2; }
                // PictureBoxControl SetControl
                _pictureBoxControl.setControl(_picturebox);
                _pictureBoxControl.setParentControl(_panel);

                void EnableDoubleBuffering(Control control)
                {
                    control.GetType().InvokeMember(
                       "DoubleBuffered",
                       BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                       null,
                       control,
                       new object[] { true });
                }
                // DoubleBuffering を有効にする
                EnableDoubleBuffering(_picturebox);
                EnableDoubleBuffering(innerControl);

                // ======== Inner Panel 設定 ========
                // Control SetObject
                // イベント用クラスにControlをセット
                _panelEvents = new ControlEvents(parentControl);
                _panelEvents.setErrorLog(_errorLog);
                _panelEvents.FileListForRead = this._fileListRead;
                // イベントメソッド設定
                _panelEvents._mouseEventHandler.OnLeftClick += ViewPrevious;
                _panelEvents._mouseEventHandler.OnRightClick += ViewNext;
                _panelEvents._mouseEventHandler.OnDragDrop += DragDrop;
                _panelEvents._mouseEventHandler.OnMouseWheel += this.MouseWheelEvent;

                _moveControlEvent = new MoveControlEvents(innerControl,paintControl);
                _moveControlEvent.setErrorLog(_errorLog);

                // フォームの大きさに追随して伸縮するようになる
                _panel.Dock = DockStyle.Fill;

                // アンカー  左上に表示
                _panel.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                // DragAndDrop を許可
                _panel.AllowDrop = true;

                // ======== PictureBox 設定 ========

                // イベント用クラスにControlをセット
                _pictureboxEvents = new ControlEvents(paintControl);
                _pictureboxEvents.setErrorLog(_errorLog);
                _pictureboxEvents.FileListForRead = this._fileListRead;

                // イベントメソッド設定
                _pictureboxEvents._mouseEventHandler.OnLeftClick += ViewPrevious;
                _pictureboxEvents._mouseEventHandler.OnRightClick += ViewNext;

                // 現状何もしていない
                ret = _pictureBoxControl.Initialize();

                // 位置変更
                //_picturebox.Location = new Point(100, 100);

                // アンカー  左上に表示
                _picturebox.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                // ※AnchorとLocationは、Locationが優先される

                //_picturebox.SizeMode = PictureBoxSizeMode.CenterImage; // コントロールの中央＝画像の中央
                _picturebox.SizeMode = PictureBoxSizeMode.Zoom; // 画像にフィット

                // フォームの大きさに追随してピクチャボックスが伸縮するようになる
                _picturebox.Dock = DockStyle.Fill;

                // ======== Frame 設定 ========
                _viewFrameControl = new ViewControl.ViewFrameControl((Panel)parentControl);
                _viewFrameControl.setErrorLog(_errorLog);
                _viewFrameControl.setParentControl(contents);
                _viewFrameControl.State = _pictureBoxControl.State;
                //_contentsControl.ViewFrameCotntrolList.Add(_viewFrameControl);

                // ======== Inner 設定 ========
                // InnerControlSetObject
                _viewInnerControl = new ViewInnerControl();
                _viewInnerControl.setErrorLog(_errorLog);
                _viewInnerControl.setControl(innerControl);
                _viewInnerControl.setParentControl(parentControl);
                _viewInnerControl.setViewImageControl(_pictureBoxControl);
                _viewFrameControl.ViewInnerControl = _viewInnerControl;

                // ======== ViewImageObjects 設定 ========
                _viewImageObjects = new ViewImageObjects();
                _viewImageObjects.setErrorLog(_errorLog);
                _viewImageObjects.FileList = this._fileList;
                _viewImageObjects.FileRegister = this._fileListRead;
                _viewImageObjects.ViewImage = new ViewImage();
                _viewImageObjects.ViewFrameControl = _viewFrameControl;
                //
                _viewImageManager = new ViewImageManager();
                _viewImageManager.ViewImageObjectList.Add(_viewImageObjects);

                // ======== Function 設定 ========
                _mainformFunction = new Function.MainFormFunction(baseForm);
                _mainformFunction.setErrorLog(_errorLog);
                _mainformFunction.setContentsControl(_contentsControl);
                Functions.MainFormFunction = _mainformFunction;
                _mainformFunction.Functions = Functions;

                // ForFunctionSetObject ViewImageControlFunction
                _viewImageControlFunction = new ViewImageControlFunction(_pictureBoxControl, _viewInnerControl, _viewFrameControl);
                _viewImageControlFunction.SetErrorLog(_errorLog);
                //_viewImageControlFunction.Function
                Functions.ControlFunction = _viewImageControlFunction;
                _viewImageControlFunction.ViewImageObjects = _viewImageObjects;

                // BasicFunctionSetObject
                _viewImageBasicFunction = new ViewImageBasicFunction();
                _viewImageBasicFunction.setErrorLog(_errorLog);
                _viewImageBasicFunction.ViewImageControl = _viewInnerControl;
                _viewImageBasicFunction.ViewImage = _viewImage;
                _viewImageBasicFunction.FileList = _fileList;
                _viewImageBasicFunction.FileRegister = _fileListRead;
                _viewImageBasicFunction.ViewImageManager = _viewImageManager;

                // BaseForm events
                _baseFormEvents = new Events.BaseFormEvents(baseForm);
                _baseFormEvents.setErrorLog(_errorLog);
                _baseFormEvents.initialize();
                _baseFormEvents.State = _viewInnerControl.State;
                _baseFormEvents.Functions = Functions;

                // inner control 
                _innerControlSizeLocationEvents = new InnerControlSizeLocationEvents(innerControl);
                _innerControlSizeLocationEvents.SetErrorLog(_errorLog);
                _innerControlSizeLocationEvents.SetRecieveControl(innerControl);
                _innerControlSizeLocationEvents.Initialize();
                _innerControlSizeLocationEvents.Functions = Functions;

                // FrameControl events
                _frameControlEvents = new Events.FrameControlEvents(parentControl);
                _frameControlEvents.SetErrorLog(_errorLog);
                _frameControlEvents.State = _viewInnerControl.State;
                _frameControlEvents.SetRecieveEventControl(_picturebox);
                _frameControlEvents.Initialize();
                _frameControlEvents.SetViewFrameControl(_viewFrameControl);
                _frameControlEvents.ViewImageObjects = _viewImageObjects;
                _frameControlEvents.Functions = Functions;
                // FrameControl Add Events
                _frameControlAddEvents = new FrameControlAddEvents(parentControl);
                _frameControlAddEvents.SetErrorLog(_errorLog);
                _frameControlAddEvents.Functions = Functions;
                _frameControlAddEvents.Initialize();

                // FrameControl Key events
                FrameControlKeyEvents = new ViewControl.FrameControlKeyEvents(parentControl);
                FrameControlKeyEvents.setErrorLog(_errorLog);
                FrameControlKeyEvents.setRecieveEventControl(baseForm);
                ret = FrameControlKeyEvents.initialize();
                FrameControlKeyEvents.Functions = Functions;
                FrameControlKeyEvents.Settings = Settings;
                // ======== Object 設定後の初期化 ========

                Functions = new Function.CommonFunctions
                {
                    BasicFunction = _viewImageBasicFunction,
                    ControlFunction = _viewImageControlFunction,
                    MainFormFunction = _mainformFunction
                };

                // initialize
                Functions.ControlFunction.ChangeSizeFrameControl(baseForm.ClientSize);
                return 1;
            }
            catch (Exception ex)
            {
                _errorLog.addException(ex, this.ToString() + " initialize");
                return 0;
            }
        }

    }
}
