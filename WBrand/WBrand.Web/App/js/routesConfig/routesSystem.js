angular
    .module('app')
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$breadcrumbProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $breadcrumbProvider) {
        $stateProvider
            .state('app.system', {
                url: "/system",
                abstract: true,
                template: '<ui-view></ui-view>',
                ncyBreadcrumb: {
                    label: 'Hệ thống'
                }
            })
            .state('app.system.slideShow', {
                url: '/slideShow',
                templateUrl: '/app/views/system/slide/list.html',
                controller: 'slideShowCtrl',
                ncyBreadcrumb: {
                    label: 'Slide hình'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/slide/list.js']
                        });
                    }]
                }
            })
            .state('app.system.addSlide', {
                url: '/addSlideShow',
                templateUrl: '/app/views/system/slide/slide.html',
                controller: 'addSlideShowCtrl',
                ncyBreadcrumb: {
                    label: 'Thêm slide hình'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/slide/add.js']
                        });
                    }]
                }
            })
            .state('app.system.editSlide', {
                url: '/editSlideShow/:id',
                templateUrl: '/app/views/system/slide/slide.html',
                controller: 'editSlideShowCtrl',
                ncyBreadcrumb: {
                    label: 'Sửa slide hình'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/slide/edit.js']
                        });
                    }]
                }
            })
    }]);
