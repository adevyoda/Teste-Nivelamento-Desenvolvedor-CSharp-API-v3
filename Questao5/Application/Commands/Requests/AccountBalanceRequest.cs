﻿namespace Questao5.Application.Queries.Requests
{
    public class AccountBalanceRequest
    {
        public string AccountId { get; set; }

        public AccountBalanceRequest(string accountId)
        {
            AccountId = accountId;
        }
    }
}
