﻿using MangoPay.SDK.Entities;

namespace MangoPay.SDK.Core.Interfaces
{
    /// <summary>Storage strategy interface.</summary>
    public interface IStorageStrategy
    {
        OAuthTokenDTO Get(string envKey);

        void Store(OAuthTokenDTO token, string envKey);
    }
}
