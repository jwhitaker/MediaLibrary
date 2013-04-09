define([
    'backbone',
    'models/Movie',
    'collections/MovieCollection',
    'views/header',
    'views/home',
    'views/movies/list',
    'views/movies/edit'
], function (Backbone, Movie, MovieCollection, HeaderView, HomeView, MovieListView, MovieEditView) {
    var AppRouter = Backbone.Router.extend({
        routes: {
            '': 'listMovies',
            'movies': 'listMovies',
            'movies/page/:page': 'listMovies',
            'movies/new': 'newMovie',
            'movies/:id': 'editMovie',
            '*actions': 'defaultRoute'
        },

        initialize: function () {
            this.headerView = new HeaderView();

            $('header').html(this.headerView.render().el);
        },

        homeView: function (actions) {
            $('#content').html(new HomeView().render().el);
        },

        listMovies: function (page) {
            var p = page ? parseInt(page, 10) : 1;
            var movieList = new MovieCollection();

            movieList.fetch({
                page: p,
                success: function (collection, response, options) {
                    $('#content').html(new MovieListView({ model: movieList }).render().el);
                }
            });
        },

        newMovie: function () {
            var $this = this;
            var model = new Movie();

            var view = new MovieEditView({ model: model });

            view.on('back', function () {
                $this.navigate('#/movies', { trigger: true });
            });

            view.model.on('save-success', function (id) {
                delete view;

                $this.navigate('#/movies/' + id);
            });

            $('#content').html(view.render().el);
        },

        editMovie: function (id) {
            var $this = this;
            var model = new Movie({ Id: id });
            var view = new MovieEditView({ model: new Movie({ Id: id })});

            view.on('back', function() {
                delete view;

                $this.navigate('#/movies');
            });

            view.model.on('save-success', function(id) {
                delete view;

                $this.navigate('#/movies/' + id, { trigger: true });
            });

            $('#content').html(view.render().el);
        }
    });

    return {
        initialize: function () {
            var app_router = new AppRouter();

            Backbone.history.start();
        }
    };
});