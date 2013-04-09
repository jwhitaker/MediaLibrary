define([
    'backbone',
    'text!templates/movie/list.html'
], function (Backbone, movieTemplate) {
    var MovieListView = Backbone.View.extend({
        initialize: function () {
            this.template = _.template(movieTemplate);
        },

        render: function () {
            this.$el.html(this.template({
                movies: this.model.toJSON(),
                pageInfo: this.model.getInfo()
            }));

            return this;
        }
    });

    return MovieListView;
});