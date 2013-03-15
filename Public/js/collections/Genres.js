define([
    'backbone',
    'models/Genre'
], function (Backbone, Genre) {
    var Genres = Backbone.Collection.extend({
        model: Genre,
        url: 'api/Genres'
    });

    return Genres;
});