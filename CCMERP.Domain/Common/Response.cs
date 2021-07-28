﻿using System.Collections.Generic;

namespace CCMERP.Domain.Common
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data, string message = null,bool _succeeded=false)
        {
            Status = _succeeded;
            Message = message;
            Data = data;
        }
        public Response(string message, bool _succeeded = false)
        {
            Status = _succeeded;
            Message = message;
        }
        public bool Status { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
