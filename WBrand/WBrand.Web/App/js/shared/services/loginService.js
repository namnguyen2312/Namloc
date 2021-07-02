(function (app) {
    'use strict';
    app.service('loginService', ['$http', '$q', 'authenticationService', 'authData', '$window',
        function ($http, $q, authenticationService, authData, $window) {
            var userInfo;
            var deferred;

            this.configLogin = function (dataInfo) {
                var info = JSON.parse(dataInfo);
                authenticationService.setTokenInfo(info);
                authData.authenticationData.IsAuthenticated = true;
                authData.authenticationData.userName = info.userName;
            }

            this.logOut = function () {
                Cookies.remove('TokenApp');
                authData.authenticationData.IsAuthenticated = false;
                authData.authenticationData.userName = "";
            }
        }]);
})(angular.module('app.common'));