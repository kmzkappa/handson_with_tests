using NXMobileHandsOn.ServiceReference;
using NXMobileHandsOn.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;

namespace NXMobileHandsOn.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {

        /// <summary>
        /// 会社コード
        /// </summary>
        [Required(ErrorMessage = "会社コードを入力してください。")]
        [StringLength(maximumLength: 3, MinimumLength = 10, ErrorMessage = "会社コードは 3～10 文字を入力してください。")]
        public ReactiveProperty<string> KaiCode { get; set; }
            = new ReactiveProperty<string>();

        // ユーザーID
        [Required(ErrorMessage = "ユーザーIDを入力してください。")]
        [StringLength(maximumLength: 3, MinimumLength = 10, ErrorMessage = "ユーザーIDは 3～10 文字を入力してください。")]
        public ReactiveProperty<string> UserId { get; set; }
            = new ReactiveProperty<string>();

        // パスワード
        [Required(ErrorMessage = "パスワードを入力してください。")]
        [StringLength(maximumLength: 3, MinimumLength = 10, ErrorMessage = "パスワードは 3～10 文字を入力してください。")]
        public ReactiveProperty<string> Password { get; set; }
            = new ReactiveProperty<string>();

        // ログイン日付
        public ReactiveProperty<DateTime> LoginDate { get; set; }
            = new ReactiveProperty<DateTime>();

        // ログインエラーメッセージ
        public ReactiveProperty<string> ErrorMessage { get; set; }
            = new ReactiveProperty<string>();

        // エラーメッセージ（会社コード）
        public ReactiveProperty<string> ErrorKaiCode { get; set; }
            = new ReactiveProperty<string>();

        // エラーメッセージ（ユーザーID）
        public ReadOnlyReactiveProperty<string> ErrorUserId { get; set; }

        // エラーメッセージ（パスワード）
        public ReadOnlyReactiveProperty<string> ErrorPassword { get; set; }

        // ログインボタンの処理
        public ReactiveCommand LoginCommand { get; set; }
            = new ReactiveCommand();

        // NavigationService
        private INavigationService _navigationService;

        public LoginPageViewModel(INavigationService navigationService)
        {
            SetValidateAttributes();
            LoginCommand.Subscribe(LoginButtonTapped);
            _navigationService = navigationService;

        }

        // ログインボタンの押下時処理
        private async void LoginButtonTapped(object sender)
        {
            var loginService = new LoginService();
            NLC00100SIParamV2 result = null;
            try
            {
                // ログインサービスの呼び出し
                result = await loginService.LoginAsync(KaiCode.Value, UserId.Value, Password.Value, LoginDate.Value);
            }
            catch (Exception ex)
            {
                ErrorMessage.Value = ex.Message;
            }

            // ログインに成功すると認証キー'NshKey'が設定される
            if (result?.NshKey != null)
            {
                ErrorMessage.Value = string.Empty;
                await _navigationService.NavigateAsync("MenuPage");
            }
            else
            {
                ErrorMessage.Value = result.clientMessageList.FirstOrDefault().StatusMessage;
            }
        }

        private void SetValidateAttributes()
        {
            KaiCode.SetValidateAttribute(() => KaiCode);
            UserId.SetValidateAttribute(() => UserId);
            Password.SetValidateAttribute(() => Password);

            ErrorKaiCode = KaiCode
                // エラーが発行されるIO<IE>を変換する
                .ObserveErrorChanged
                // エラーがない場合nullになるので空のIEにする
                .Select(e => e ?? Enumerable.Empty<object>())
                // 最初のエラーメッセージを取得する
                .Select(e => e.OfType<string>().FirstOrDefault())
                // ReactiveProperty化
                .ToReactiveProperty();

            ErrorUserId = UserId
                .ObserveErrorChanged
                .Select(e => e ?? Enumerable.Empty<object>())
                .Select(e => e.OfType<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();

            ErrorPassword = Password
                .ObserveErrorChanged
                .Select(e => e ?? Enumerable.Empty<object>())
                .Select(e => e.OfType<string>().FirstOrDefault())
                .ToReadOnlyReactiveProperty();
        }
    }
}
