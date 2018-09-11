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
            .state('app.system.info', {
                url: '/slideShow',
                templateUrl: '/app/views/system/info/item.html',
                controller: 'systemInfoCtrl',
                ncyBreadcrumb: {
                    label: 'Thông tin hệ thống'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/info/item.js']
                        });
                    }]
                }
            })
            .state('app.system.banner', {
                url: '/banner',
                templateUrl: '/app/views/system/banner/list.html',
                controller: 'bannerCtrl',
                ncyBreadcrumb: {
                    label: 'Danh sách banner đối tác'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/banner/list.js']
                        });
                    }]
                }
            })
            .state('app.system.addBanner', {
                url: '/addBanner',
                templateUrl: '/app/views/system/banner/banner.html',
                controller: 'addBannerCtrl',
                ncyBreadcrumb: {
                    label: 'Thêm banner'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/banner/add.js']
                        });
                    }]
                }
            })
            .state('app.system.editBanner', {
                url: '/editBanner/:id',
                templateUrl: '/app/views/system/banner/banner.html',
                controller: 'editBannerCtrl',
                ncyBreadcrumb: {
                    label: 'Sửa banner'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/banner/edit.js']
                        });
                    }]
                }
            })
            .state('app.system.video', {
                url: '/video',
                templateUrl: '/app/views/system/video/list.html',
                controller: 'videoCtrl',
                ncyBreadcrumb: {
                    label: 'Danh sách video'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/video/list.js']
                        });
                    }]
                }
            })
            .state('app.system.addVideo', {
                url: '/addVideo',
                templateUrl: '/app/views/system/video/video.html',
                controller: 'addVideoCtrl',
                ncyBreadcrumb: {
                    label: 'Thêm video mới'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/video/add.js']
                        });
                    }]
                }
            })
            .state('app.system.editVideo', {
                url: '/editVideo/:id',
                templateUrl: '/app/views/system/video/video.html',
                controller: 'editVideoCtrl',
                ncyBreadcrumb: {
                    label: 'Sửa video'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/system/video/edit.js']
                        });
                    }]
                }
            })
    }]);
