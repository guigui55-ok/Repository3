using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageViewer2
{
    public class PictureBoxState : IViewControlState
    {
        private bool _isFrameChanging = false;
        private bool _nowSizeUpdate;
        private bool _isPausePaint;
        private bool _isMoseHover;
        private PointF _differenceSizeFromContents;
        private PointF _differencePositionFromContents;
        private Point _differenceSizeInnerFromFrame;
        private Point _differencePositionInnerInFrame;
        private PointF _ratioSizeInnerFromFrame;
        private PointF _ratioLocationInnerFromFrame;
        private double _ratioLocationInnerFromFrameX;
        private double _ratioLocationInnerFromFrameY;

        private bool _isActiveControl;
        public bool IsActiveControl { get => _isActiveControl; set => _isActiveControl = value; }
        public bool IsMouseHover { get => _isMoseHover; set => _isMoseHover = value; }
        public PictureBoxState()
        {
            _isActiveControl = true;
        }
        public PointF DifferenceSizeFromContents
        {
            get { return _differenceSizeFromContents; }
            set
            {
                _differenceSizeFromContents.X = value.X;
                _differenceSizeFromContents.Y = value.Y;
            }
        }
        public PointF DifferencePositionFromContents
        {
            get { return _differencePositionFromContents; }
            set
            {
                _differencePositionFromContents.X = value.X;
                _differencePositionFromContents.Y = value.Y;
            }
        }
        
        bool IViewControlState.NowSizeUpdate { get => _nowSizeUpdate; set => _nowSizeUpdate = value; }
        bool IViewControlState.IsPausePaint { get => _isPausePaint; set => _isPausePaint = value; }
        public Point DifferenceSizeInnerFromFrame { get => _differenceSizeInnerFromFrame; set => _differenceSizeInnerFromFrame = value; }
        public Point DifferencePositionInnerInFrame { get => _differencePositionInnerInFrame; set => _differencePositionInnerInFrame = value; }
        public PointF RatioSizeInnerFromFrame { get => _ratioSizeInnerFromFrame; set => _ratioSizeInnerFromFrame = value; }
        public PointF RatioLocationInnerFromFrame { get => _ratioLocationInnerFromFrame; set => _ratioLocationInnerFromFrame = value; }
        public double RatioLocationInnerFromFrameX { get => _ratioLocationInnerFromFrameX; set => _ratioLocationInnerFromFrameX = value; }
        public double RatioLocationInnerFromFrameY { get => _ratioLocationInnerFromFrameY; set => _ratioLocationInnerFromFrameY = value; }
        public bool IsFrameSizeChanging { get => _isFrameChanging; set => _isFrameChanging = value; }
    }
}
