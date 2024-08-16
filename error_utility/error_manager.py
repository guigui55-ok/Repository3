from _typeshed import ReadOnlyBuffer

from file_io.error_utility.log_manager import log_manager


class error_manager():
    debug_mode = 0
    logs
    def __init__(self,debug_mode) -> None:
        self.debug_mode = debug_mode
    def __init__(self,debug_mode,log_directory,error_log_file_name,log_file_name) -> None:
        self.logs = log_manager(self,log_directory,log_file_name)


class error_constants():
    TYPE_UNEXPECTED_ERROR = 1
    TYPE_ERROR = 2
    TYPE_WORNING = 3
    TYPE_ALERT = 4
    DEFAULT_WINDOW_TITLE = 'ERROR!'
