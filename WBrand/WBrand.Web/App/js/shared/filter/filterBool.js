(function (app) {

    app.filter('filterBoolText', function () {
        return function (input) {
            if (input == true)
                return 'Mở';
            else
                return 'Đóng';
        }
    });

    app.filter('filterBoolClass', function () {
        return function (input) {
            if (input == true)
                return 'badge badge-success';
            else
                return 'badge badge-warning';
        }
    });


})(angular.module('app.common'));