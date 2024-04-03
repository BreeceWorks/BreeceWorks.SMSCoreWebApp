using BreeceWorks.Shared.Entities;
using BreeceWorks.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BreeceWorks.Shared.CaseObjects
{
    public class Message
    {
        private string _id;
        private string? _sMSId;
        private MessageType? _type;
        private MessageFormatting? _formatting;
        private string _data;
        private string _status;
        private MessageChannelSource? _channelSource;
        private MessageAuthor? _author;
        private System.DateTime? _createdAt;
        private bool? _needsAttention;
        private bool? _needsAction;
        private List<MessageAttachment>? _messageAttachments;

        public string Id
        {
            get { return _id; }

            set
            {
                if (_id != value)
                {
                    _id = value;
                }
            }
        }

        public string? SMSId
        {
            get { return _sMSId; }

            set
            {
                if (_sMSId != value)
                {
                    _sMSId = value;
                }
            }
        }

        public MessageType? Type
        {
            get { return _type; }

            set
            {
                if (_type != value)
                {
                    _type = value;
                }
            }
        }

        public MessageFormatting? Formatting
        {
            get { return _formatting; }

            set
            {
                if (_formatting != value)
                {
                    _formatting = value;
                }
            }
        }

        public string Data
        {
            get { return _data; }

            set
            {
                if (_data != value)
                {
                    _data = value;
                }
            }
        }

        public string Status
        {
            get { return _status; }

            set
            {
                if (_status != value)
                {
                    _status = value;
                }
            }
        }

        public MessageChannelSource? ChannelSource
        {
            get { return _channelSource; }

            set
            {
                if (_channelSource != value)
                {
                    _channelSource = value;
                }
            }
        }

        public MessageAuthor? Author
        {
            get { return _author; }

            set
            {
                if (_author != value)
                {
                    _author = value;
                }
            }
        }

        public System.DateTime? CreatedAt
        {
            get { return _createdAt; }

            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                }
            }
        }

        public bool? NeedsAttention
        {
            get { return _needsAttention; }

            set
            {
                if (_needsAttention != value)
                {
                    _needsAttention = value;
                }
            }
        }

        public bool? NeedsAction
        {
            get { return _needsAction; }

            set
            {
                if (_needsAction != value)
                {
                    _needsAction = value;
                }
            }
        }

        public List<MessageAttachment>? MessageAttachments
        {
            get { return _messageAttachments; }

            set
            {
                if (_messageAttachments != value)
                {
                    _messageAttachments = value;
                }
            }
        }

    }
}
