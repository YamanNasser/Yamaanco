using System;
using Yamaanco.Domain.Enums;

namespace Yamaanco.Application.DTOs.Notification
{
    public class NotificationDto
    { 
        public string Id { get; set; }
        public string ParticipantId { get; set; }
        public string ParticipantName { get; set; }
        public string ParticipantPhotoPath { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Target { get; set; }
        public NotificationCategory CategoryType { get; set; }
        public bool IsSeen { get; set; }
        public DateTime CreatedDate { get; set; }
        public int GenderId { get; set; }
        public string Content { get; set; }
    }
}