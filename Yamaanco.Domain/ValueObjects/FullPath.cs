using System.Collections.Generic;
using Yamaanco.Domain.Common;

namespace Yamaanco.Domain.Entities.BaseEntities
{
    public class FullPath : ValueObject
    {
        private string Path { get; set; }
        private string CategoryId { get; set; }
        private string Extension { get; set; }

        public FullPath(string path, string categoryId, string extension)
        {
            Path = path;
            CategoryId = categoryId;
            Extension = extension;
        }

        public override string ToString()
        {
            return $"{Path}\\{CategoryId}{Extension}";
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Path;
            yield return CategoryId;
            yield return Extension;
        }
    }
}