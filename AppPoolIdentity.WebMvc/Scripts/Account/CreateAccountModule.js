///<reference path="~/Scripts/jquery-1.10.2.intellisense.js"></reference>

var CreateAccountModule = ( function(){ 
    var _elements = {
        txtUserName : "#UserName",
        txtEmail : "#Email",
        txtPassword : "#Password",
        txtConfirmPassword : "#ConfirmPassword",
        btnCreateAccount : "button[type=submit]",
        btnBackToHome : ".btn.btn-danger"
    };
    
    var _settings = {
        createAccountUrl : '',
        homeUrl : ''
    };

    function _bindEvents(){
        jQuery(_elements.btnCreateAccount).on('click',function(){
            jQuery(_elements.btnCreateAccount).hide();
            jQuery(_elements.btnBackToHome).hide();
    
            var createAccountModel = {
                userName : jQuery.trim( jQuery(_elements.txtUserName).val()),
                email : jQuery.trim( jQuery(_elements.txtEmail).val()),
                password : jQuery.trim( jQuery(_elements.txtPassword).val()),
                confirmPassword : jQuery.trim( jQuery(_elements.txtConfirmPassword).val()),
            };

            if(createAccountModel.userName == ''){
                alert('Enter User Name');
                return false;
            }
            if(createAccountModel.email == ''){
                alert('Enter Email');
                return false;
            }
            if(createAccountModel.password == ''){
                alert('Enter Password');
                return false;
            }
            if(createAccountModel.confirmPassword == ''){
                alert('Enter Confirm Password');
                return false;
            }
            
            jQuery.ajax({
                type: 'POST',
                url: (_settings.createAccountUrl),
                dataType: 'json',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(createAccountModel),
                async: true
            }).done(function (response) {
                alert(response.Message);
                if(response.ok)
                    window.location(_settings.homeUrl);
            }).fail(function (response){
                alert(response.Message);
            }).always(function (){
                jQuery(_elements.btnCreateAccount).show();
                jQuery(_elements.btnBackToHome).show();
            });          

            return false;
        });
    };

    function _initialize(settings){
        _settings.createAccountUrl = settings.createAccountUrl;
        _settings.homeUrl = settings.homeUrl;
        //
        _bindEvents();
    };

    return {
        ini: function(settings){
            _initialize(settings);
        }
    };
})();