require.config({
    paths: {
        jquery: 'libs/jquery/jquery-1.9.1',
        underscore: 'libs/underscore/underscore-min',
        backbone: 'libs/backbone/backbone',
        bootstrap: 'libs/bootstrap/js/bootstrap.min',
        utils: 'libs/utils',
        templates: '../templates'
    },
    shim: {
        'backbone': {
            deps: ['underscore', 'jquery'],
            exports: 'Backbone'
        },
        'underscore': {
            exports: '_'
        },
        'jquery': {
            exports: '$'
        },
        'bootstrap': ['jquery'],
        'utils': ['underscore'],
        'app': ['bootstrap', 'utils']
    }
});

require([
    'app'
], function (App) {
    App.initialize();
});