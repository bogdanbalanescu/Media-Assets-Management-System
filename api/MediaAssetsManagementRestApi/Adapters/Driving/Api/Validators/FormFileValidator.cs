using Api.Validators.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Api.Validators
{
    public class FormFileValidator
    {
        private IReadOnlyCollection<string> acceptedImageExtensions = new List<string>
        {
            ".PNG",
            ".JPG"
        };
        private IReadOnlyCollection<string> acceptedVideoExtensions = new List<string>
        {
            ".AVI"
        };

        internal void ValidateForImage(IFormFile imageFile)
        {
            ValidateForFile(imageFile);

            if (!FileNameHasValidExtension(imageFile.FileName, acceptedImageExtensions))
                throw new FileExtensionIsNotAcceptedRequestException();
        }

        internal void ValidateForVideo(IFormFile imageFile)
        {
            ValidateForFile(imageFile);

            if (!FileNameHasValidExtension(imageFile.FileName, acceptedVideoExtensions))
                throw new FileExtensionIsNotAcceptedRequestException();
        }

        private void ValidateForFile(IFormFile imageFile)
        {
            if (imageFile.Length == 0)
                throw new FileIsEmptyRequestException();

            if (FileNameContainsInvalidCharacters(imageFile.FileName))
                throw new FileNameContainsInvalidCharactersRequestException();

            var wasContentTypeDetermined = new FileExtensionContentTypeProvider().TryGetContentType(imageFile.FileName, out var mimeTypeFromFile);
            if (!wasContentTypeDetermined || imageFile.ContentType != mimeTypeFromFile)
                throw new FileContentTypeIsNotAcceptedRequestException();
        }

        private bool FileNameContainsInvalidCharacters(string fileName)
        {
            var illegalFileCharacters = new string(Path.GetInvalidFileNameChars());
            return Regex.IsMatch(fileName, illegalFileCharacters, RegexOptions.CultureInvariant);
        }

        private bool FileNameHasValidExtension(string fileName, IReadOnlyCollection<string> acceptedExtensions)
        {
            if (string.IsNullOrEmpty(fileName)) return false;

            var extension = Path.GetExtension(fileName);
            if (string.IsNullOrEmpty(extension)) return false;

            return acceptedExtensions.Contains(extension.ToUpperInvariant());
        }
    }
}
