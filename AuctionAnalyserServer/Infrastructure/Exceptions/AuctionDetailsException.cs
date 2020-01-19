using System;

namespace AuctionAnalyserServer.Infrastructure.Exceptions
{
    public class AuctionDetailsException : AuctionAnalyserException
    {
        public AuctionDetailsException()
        {
        }

        public AuctionDetailsException(string code) : base(code)
        {
        }

        public AuctionDetailsException(string message, params object[] args) : base(string.Empty, message, args)
        {
        }

        public AuctionDetailsException(string code, string message, params object[] args) : base(null, code, message, args)
        {
        }

        public AuctionDetailsException(Exception innerException, string message, params object[] args)
            : base(innerException, string.Empty, message, args)
        {
        }

        public AuctionDetailsException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
        }

    }
}