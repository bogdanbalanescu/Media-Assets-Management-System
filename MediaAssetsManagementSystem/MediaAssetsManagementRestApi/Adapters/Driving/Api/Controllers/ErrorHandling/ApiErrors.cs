using System;

namespace Api.Controllers.ErrorHandling
{
    [Serializable]
    public class ApiErrors : EnumerationClass<int, string>
    {
        public static ApiErrors FolderNameMustBeUniqueInParent = new ApiErrors(1000, "Folder name must be unique in parent.");
        public static ApiErrors FileIsEmpty = new ApiErrors(1001, "File cannot be empty.");
        public static ApiErrors FileContainsInvalidCharacters = new ApiErrors(1002, "File cannot contain invalid characters.");
        public static ApiErrors FileExtensionIsNotAccepted = new ApiErrors(1003, "File extension is not accepted.");
        public static ApiErrors FileContentTypeIsNotAccepted = new ApiErrors(1004, "File content type is not accepted.");

        public ApiErrors(int id, string value) : base(id, value)
        {
        }

    }
}
