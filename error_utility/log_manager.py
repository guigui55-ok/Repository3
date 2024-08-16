class log_manager():
    log_directory = ''
    log_file_name = ''
    err = null
    def __init__(self,error_manager,log_directory,log_file_name) -> None:
        self.err = error_manager
        self.log_directory = log_directory
        self.log_file_name = log_file_name
    