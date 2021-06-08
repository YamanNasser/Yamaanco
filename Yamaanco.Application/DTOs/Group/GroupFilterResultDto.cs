using System;
using System.Collections.Generic;
using Yamaanco.Application.DTOs.Common;

namespace Yamaanco.Application.DTOs.Group
{
    public class GroupFilterResultDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int GroupTypeId { get; set; }
        public IEnumerable<PhotoResourcesDto> GroupPhotoResources { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public int NumberOfViewer { get; set; }
        public int NumberOfMember { get; set; }
    }
}