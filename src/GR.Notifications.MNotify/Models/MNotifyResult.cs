using System;
using System.Collections.Generic;
using System.Linq;

namespace GR.Notifications.MNotify.Models
{
    public class MNotifyResult<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public ICollection<string> Errors { get; } = new List<string>();
        public bool HasException { get; set; }
        public Exception Exception { get; set; }
        public string ErrorMessage => Errors.Aggregate((p, n) => p + "; " + n);
    }
}