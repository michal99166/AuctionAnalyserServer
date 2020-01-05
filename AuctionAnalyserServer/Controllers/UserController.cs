using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionAnalyserServer.Base.Controller;
using AuctionAnalyserServer.Base.CQRS.Mediator;
using Microsoft.AspNetCore.Mvc;

namespace AuctionAnalyserServer.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController(ICqrsMediator dataBus) : base(dataBus)
        {
        }
    }
}