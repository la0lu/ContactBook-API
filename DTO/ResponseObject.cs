﻿namespace ContactBook.DTO
{
    public class ResponseObject<TData, TError>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int Code { get; set; }
        public TError Errors { get; set; }

        public TData Data { get; set; }

    }

}
