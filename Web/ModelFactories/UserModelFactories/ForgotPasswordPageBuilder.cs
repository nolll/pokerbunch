﻿using Application.UseCases.AppContext;
using Web.Models.PageBaseModels;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ForgotPasswordPageBuilder : IForgotPasswordPageBuilder
    {
        private readonly IAppContextInteractor _appContextInteractor;

        public ForgotPasswordPageBuilder(IAppContextInteractor appContextInteractor)
        {
            _appContextInteractor = appContextInteractor;
        }

        private ForgotPasswordPageModel Create()
        {
            var contextResult = _appContextInteractor.Execute();

            return new ForgotPasswordPageModel
            {
                BrowserTitle = "Forgot Password",
                PageProperties = new PageProperties(contextResult)
            };
        }

        public ForgotPasswordPageModel Build(ForgotPasswordPostModel postModel)
        {
            var model = Create();
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }

        public ForgotPasswordConfirmationPageModel BuildConfirmation()
        {
            var contextResult = _appContextInteractor.Execute();

            return new ForgotPasswordConfirmationPageModel
            {
                BrowserTitle = "Password Sent",
                PageProperties = new PageProperties(contextResult)
            };
        }
    }
}