angular
    .module('app')
    .config(['$stateProvider', '$urlRouterProvider', '$ocLazyLoadProvider', '$breadcrumbProvider', function ($stateProvider, $urlRouterProvider, $ocLazyLoadProvider, $breadcrumbProvider) {
        $stateProvider
            .state('app.users', {
                url: '/userList',
                templateUrl: '/app/views/users/userList.html',
                ncyBreadcrumb: {
                    label: 'User list'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/users/userList.js']
                        });
                    }]
                }
            })
            .state('app.addUser', {
                url: '/addUser',
                templateUrl: '/app/views/users/user.html',
                controller: 'addUserCtrl',
                ncyBreadcrumb: {
                    label: 'Add user'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/users/addUser.js']
                        });
                    }]
                }
            })
            .state('app.editUser', {
                url: '/editUser/:id',
                templateUrl: '/app/views/users/user.html',
                controller: 'editUserCtrl',
                ncyBreadcrumb: {
                    label: 'Edit user'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/users/editUser.js']
                        });
                    }]
                }
            })
            .state('app.roleGroups', {
                url: '/roleGroupList',
                templateUrl: '/app/views/roleGroups/roleGroupList.html',
                controller: 'roleGroupCtrl',
                ncyBreadcrumb: {
                    label: 'Role group'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/roleGroups/roleGroupList.js']
                        });
                    }]
                }
            })
            .state('app.addRoleGroup', {
                url: '/addRoleGroup',
                templateUrl: '/app/views/roleGroups/roleGroup.html',
                controller: 'addRoleGroupCtrl',
                ncyBreadcrumb: {
                    label: 'Add role group'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/roleGroups/addRoleGroup.js']
                        });
                    }]
                }
            })
            .state('app.editRoleGroup', {
                url: '/editRoleGroup/:id',
                templateUrl: '/app/views/roleGroups/roleGroup.html',
                controller: 'editRoleGroupCtrl',
                ncyBreadcrumb: {
                    label: 'edit role group'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/roleGroups/editRoleGroup.js']
                        });
                    }]
                }
            })
            .state('app.roles', {
                url: '/roleList',
                templateUrl: '/app/views/roles/roleList.html',
                controller: 'roleCtrl',
                ncyBreadcrumb: {
                    label: 'Role list'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/roles/roleList.js']
                        });
                    }]
                }
            })
            .state('app.category', {
                url: '/category',
                templateUrl: '/app/views/catalog/category/categoryList.html',
                controller: 'categoryCtrl',
                ncyBreadcrumb: {
                    label: 'Danh mục sản phẩm'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/catalog/category/categoryList.js']
                        });
                    }]
                }
            })
            .state('app.icons', {
                url: "/icons",
                abstract: true,
                template: '<ui-view></ui-view>',
                ncyBreadcrumb: {
                    label: 'Icons'
                }
            })
            .state('app.icons.flags', {
                url: '/flags',
                templateUrl: '/app/views/icons/flags.html',
                ncyBreadcrumb: {
                    label: 'Flags'
                }
            })
            .state('app.icons.fontawesome', {
                url: '/font-awesome',
                templateUrl: '/app/views/icons/font-awesome.html',
                ncyBreadcrumb: {
                    label: 'Font Awesome'
                }
            })
            .state('app.icons.simplelineicons', {
                url: '/simple-line-icons',
                templateUrl: '/app/views/icons/simple-line-icons.html',
                ncyBreadcrumb: {
                    label: 'Simple Line Icons'
                }
            })
            .state('app.components', {
                url: "/components",
                abstract: true,
                template: '<ui-view></ui-view>',
                ncyBreadcrumb: {
                    label: 'Components'
                }
            })
            .state('app.components.buttons', {
                url: '/buttons',
                templateUrl: '/app/views/components/buttons.html',
                ncyBreadcrumb: {
                    label: 'Buttons'
                }
            })
            .state('app.components.social-buttons', {
                url: '/social-buttons',
                templateUrl: '/app/views/components/social-buttons.html',
                ncyBreadcrumb: {
                    label: 'Social Buttons'
                }
            })
            .state('app.components.cards', {
                url: '/cards',
                templateUrl: '/app/views/components/cards.html',
                ncyBreadcrumb: {
                    label: 'Cards'
                }
            })
            .state('app.components.forms', {
                url: '/forms',
                templateUrl: '/app/views/components/forms.html',
                ncyBreadcrumb: {
                    label: 'Forms'
                }
            })
            .state('app.components.switches', {
                url: '/switches',
                templateUrl: '/app/views/components/switches.html',
                ncyBreadcrumb: {
                    label: 'Switches'
                }
            })
            .state('app.components.tables', {
                url: '/tables',
                templateUrl: '/app/views/components/tables.html',
                ncyBreadcrumb: {
                    label: 'Tables'
                }
            })
            .state('app.widgets', {
                url: '/widgets',
                templateUrl: '/app/views/widgets.html',
                ncyBreadcrumb: {
                    label: 'Widgets'
                },
                resolve: {
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load controllers
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/widgets.js']
                        });
                    }]
                }
            })
            .state('app.charts', {
                url: '/charts',
                templateUrl: '/app/views/charts.html',
                ncyBreadcrumb: {
                    label: 'Charts'
                },
                resolve: {
                    // Plugins loaded before
                    // loadPlugin: ['$ocLazyLoad', function ($ocLazyLoad) {
                    //     return $ocLazyLoad.load([
                    //         {
                    //             serial: true,
                    //             files: ['js/libs/Chart.min.js', 'js/libs/angular-chart.min.js']
                    //         }
                    //     ]);
                    // }],
                    loadMyCtrl: ['$ocLazyLoad', function ($ocLazyLoad) {
                        // you can lazy load files for an existing module
                        return $ocLazyLoad.load({
                            files: ['/app/js/controllers/charts.js']
                        });
                    }]
                }
            })
    }]);
