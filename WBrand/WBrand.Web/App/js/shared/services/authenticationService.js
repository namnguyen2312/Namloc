(function (app) {
    'use strict';
    app.service('authenticationService', ['$http', '$q', '$window', 'authData',
        function ($http, $q, $window, authData) {
            var tokenInfo;

            this.setTokenInfo = function (data) {
                tokenInfo = data;
            };

            this.getTokenInfo = function () {
                return tokenInfo;
            }

            this.removeToken = function () {
                tokenInfo = null;
                Cookies.remove("TokenInfo");
            };

            this.init = function () {
                if (Cookies.get("TokenApp")) {
                    tokenInfo = JSON.parse(Cookies.get("TokenApp"));
                    authData.authenticationData.IsAuthenticated = true;
                    authData.authenticationData.userName = tokenInfo.userName;
                }
            };

            this.setHeader = function () {
                delete $http.defaults.headers.common['X-Requested-With'];
                if ((tokenInfo != undefined) && (tokenInfo.access_token != undefined) && (tokenInfo.access_token != null) && (tokenInfo.access_token != "")) {
                    $http.defaults.headers.common['Authorization'] = 'Bearer ' + tokenInfo.access_token;
                    $http.defaults.headers.common['Content-Type'] = 'application/x-www-form-urlencoded;charset=utf-8';
                }
            }

            this.init();
        }
    ]);
})(angular.module('app.common'));