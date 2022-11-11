﻿namespace BlazorBasic.Shared
{
    public class Request
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string CustomerName { get; set; }
        public string BillNumber { get; set; }
        public double Amount { get; set; }

        public Boolean Completed { get; set; }
    }
}
