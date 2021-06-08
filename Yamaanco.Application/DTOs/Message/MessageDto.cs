using System;
using Yamaanco.Application.DTOs.Common;

namespace Yamaanco.Application.DTOs.Message
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantPictureUrl { get; set; }
        public string CategoryPictureUrl { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ViewCount { get; set; }
        public string Target { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

        public ResourcesBasicInfoDto File { get; set; }
    }
}