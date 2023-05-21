﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.Client
{
    // TODO 4: unauthorized users should receive 401 status code
    public class AccountController : AbstractClientController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [Authorize] 
        [HttpGet]
        public ValueTask<Account> Get()
        {
            return _accountService.LoadOrCreateAsync(null /* TODO 3: Get user id from cookie */);
        }

        //TODO 5: Endpoint should works only for users with "Admin" Role
        [Authorize]
        [HttpGet("{id}")]
        public Account GetByInternalId([FromRoute] int id)
        {
            return _accountService.GetFromCache(id);
        }

        [Authorize]
        [HttpPost("counter")]
        public async Task UpdateAccount()
        {
            //Update account in cache, don't bother saving to DB, this is not an objective of this task.
            var account = await Get();
            account.Counter++;
        }
    }
}