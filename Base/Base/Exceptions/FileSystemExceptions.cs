using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wK_Manager.Base.Exceptions {

    public class DirectoryAlreadyExistsException : IOException {

        public DirectoryAlreadyExistsException() {
        }

        public DirectoryAlreadyExistsException(string? message) : base(message) {
        }

        public DirectoryAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException) {
        }

        public DirectoryAlreadyExistsException(string? message, int hresult) : base(message, hresult) {
        }

        public DirectoryAlreadyExistsException(string? message, string? directory) : base(message, new Exception(directory)) {
        }
    }
}
