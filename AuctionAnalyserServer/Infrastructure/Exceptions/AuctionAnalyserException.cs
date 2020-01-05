using System;

namespace AuctionAnalyserServer.Infrastructure.Exceptions
{
    public class AuctionAnalyserException : Exception
    {
        public string Code { get; }

        protected AuctionAnalyserException()
        {
        }

        protected AuctionAnalyserException(string code)
        {
            Code = code;
        }

        protected AuctionAnalyserException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected AuctionAnalyserException(string code, string message, params object[] args) : this(null, code,
            message, args)
        {
        }

        protected AuctionAnalyserException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected AuctionAnalyserException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}