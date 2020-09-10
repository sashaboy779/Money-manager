using MoneyManagerUi.Data.Wallet;
using MoneyManagerUi.Shared.Classes;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using WalletModel = MoneyManagerUi.Data.Wallet.Wallet;

namespace MoneyManagerUi.Pages.Wallet
{
    public class WalletFormComponent : ModalComponent<WalletModel>
    {
        [Parameter]
        public string SubmitText { get; set; }

        protected IEnumerable<object> currencies;

        protected override void OnInitialized()
        {
            currencies = Enum.GetValues(typeof(Currency)).Cast<object>().Skip(1);
            base.OnInitialized();
        }
    }
}
