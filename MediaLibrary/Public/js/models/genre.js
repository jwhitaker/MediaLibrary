define([
    'backbone'
], function (Backbone) {
    var GenreModel = Backbone.Model.extend({
        idAttribute: 'Id',
        urlRoot: 'api/Genres',
        defaults: {
            Name: 'Genre Name'
        }
    });

    return GenreModel;
});