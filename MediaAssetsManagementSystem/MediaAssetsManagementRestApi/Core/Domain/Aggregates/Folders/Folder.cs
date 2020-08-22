using Domain.SharedKernel;
using Domain.SharedKernel.Exceptions;
using EnsureThat;
using System;

namespace Domain.Aggregates.Folders
{
    /// <summary>
    /// Folder aggregate. Defines a folder in the whole folder structure. May contain a parent reference.
    /// </summary>
    public class Folder : Aggregate<int>
    {
        /// <summary>
        /// Folder name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Parent Folder Identifier
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Folder constructor.
        /// </summary>
        /// <param name="name">Folder Name. Required parameter.</param>
        /// <param name="parentId">Parent Folder Identifier. Optional parameter. When optional, this folder has no parent.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        public Folder(string name, int? parentId = null)
        {
            SetName(name);
            ParentId = parentId;
        }

        /// <summary>
        /// Folder constructor.
        /// </summary>
        /// <param name="id">Folder Identifier. Required parameter.</param>
        /// <param name="creationDate">Creation Date. Required parameter.</param>
        /// <param name="name">Folder Name. Required parameter.</param>
        /// <param name="parentId">Parent Folder Identifier. Optional parameter. When optional, this folder has no parent.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        public Folder(int id, DateTime creationDate, string name, int? parentId = null)
            : base (id, creationDate)
        {
            SetName(name);
            ParentId = parentId;
        }

        /// <summary>
        /// Set the folder name.
        /// </summary>
        /// <param name="name">Folder Name. Required parameter.</param>
        /// <exception cref="RequiredArgumentException">Thrown if required arguments are null or empty.</exception>
        private void SetName(string name)
        {
            EnsureArg.IsNotNullOrEmpty(name, nameof(name),
                o => o.WithException(new RequiredArgumentException(nameof(name))));
            Name = name;
        }
    }
}
