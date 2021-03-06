angular
    .module('app')
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$breadcrumbProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $breadcrumbProvider) {

        $urlRouterProvider.otherwise('/dashboard');

        $ocLazyLoadProvider.config({
            // Set to true if you want to see what and when is dynamically loaded
            debug: true
        });

        $breadcrumbProvider.setOptions({
            prefixStateName: 'app.main',
            includeAbstract: true,
            template: '<li class="breadcrumb-item" ng-repeat="step in steps" ng-class="{active: $last}" ng-switch="$last || !!step.abstract"><a ng-switch-when="false" href="{{step.ncyBreadcrumbLink}}">{{step.ncyBreadcrumbLabel}}</a><span ng-switch-when="true">{{step.ncyBreadcrumbLabel}}</span></li>'
        });

        $stateProvider
            .state('app', {
                abstract: true,
                templateUrl: '/app/views/common/layouts/full.html',
                //page title goes here
                ncyBreadcrumb: {
                    label: 'Root',
                    skip: true
                },
                resolve: {
                    loadCSS: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load CSS files
                        return $ocLazyLoad.load([{
                            serie: true,
                            name: 'Flags',
                            files: ['/assets/libs/flag-icon-css/css/flag-icon.min.css']
                        }, {
                            serie: true,
                            name: 'Font Awesome',
                            files: ['/assets/libs/font-awesome/css/font-awesome.min.css']
                        }, {
                            serie: true,
                            name: 'Simple Line Icons',
                            files: ['/assets/libs/simple-line-icons/css/simple-line-icons.css']
                        }]);
                    }],
                    loadPlugin: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load files for an existing module
                        return $ocLazyLoad.load([{
                            serie: true,
                            name: 'chart.js',
                            files: [
                                '/assets/libs/chart.js/dist/Chart.min.js',
                                '/assets/libs/angular-chart.js/dist/angular-chart.min.js'
                            ]
                        }]);
                    }],
                }
            })
            .state('app.main', {
                url: '/dashboard',
                templateUrl: '/app/views/main.html',
                //page title goes here
                ncyBreadcrumb: {
                    label: 'Home',
                },
                //page subtitle goes here
                params: { subtitle: 'Welcome to ROOT powerfull Bootstrap & AngularJS UI Kit' },
                resolve: {
                    loadPlugin: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load files for an existing module
                        return $ocLazyLoad.load([
                            {
                                serie: true,
                                name: 'chart.js',
                                files: [
                                    '/assets/libs/chart.js/dist/Chart.min.js',
                                    '/assets/libs/angular-chart.js/dist/angular-chart.min.js'
                                ]
                            },
                        ]);
                    }],
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load controllers
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/main.js']
                        });
                    }]
                }
            })
            .state('appSimple', {
                abstract: true,
                templateUrl: '/app/views/common/layouts/simple.html',
                resolve: {
                    loadCSS: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load CSS files
                        return $ocLazyLoad.load([{
                            serie: true,
                            name: 'Font Awesome',
                            files: ['/assets/libs/font-awesome/css/font-awesome.min.css']
                        }, {
                            serie: true,
                            name: 'Simple Line Icons',
                            files: ['/assets/libs/simple-line-icons/css/simple-line-icons.css']
                        }]);
                    }],
                }
            })

            // Additional Pages
            .state('appSimple.login', {
                url: '/login',
                templateUrl: '/app/views/pages/login.html'
            })
            .state('appSimple.register', {
                url: '/register',
                templateUrl: '/app/views/pages/register.html'
            })
            .state('appSimple.404', {
                url: '/404',
                templateUrl: '/app/views/pages/404.html'
            })
            .state('appSimple.500', {
                url: '/500',
                templateUrl: '/app/views/pages/500.html'
            })
    }]);
