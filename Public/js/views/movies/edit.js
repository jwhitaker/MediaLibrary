define([
	'backbone',
    'models/movie',
    'collections/genres',
	'text!templates/movie/edit.html'
], function (Backbone, Movie, GenreCollection, movieEditTemplate) {
    var createCallback = function (limit, fn) {
        var finishedCalls = 0;

        return function () {
            if (++finishedCalls == limit) {
                fn();
            }
        };
    };

    var Callbacks = function () {
        return {
            registerMethod: function () {
            },

            execute: function () {
            }
        }
    };

    var MovieEditView = Backbone.View.extend({
        events: {
            'click .save-btn': 'saveMovie',
            'click .back-btn': 'cancel'
        },

        initialize: function () {
            this.template = _.template(movieEditTemplate);
            this.genres = new GenreCollection();
        },

        loadMovie: function (cb) {
            if (this.model.isNew()) {
                cb();
            } else {
                this.model.fetch({
                    success: function () {
                        cb();
                    }
                });
            }
        },

        loadGenres: function (cb) {
            this.genres.fetch({
                success: function () {
                    cb();
                }
            });
        },

        render: function () {
            var $this = this;
            
            var cb = createCallback(2, function () {
                $this.$el.html($this.template({ movie: $this.model.toJSON(), genres: $this.genres.toJSON() }));
            });
            
            this.loadGenres(cb);
            this.loadMovie(cb);

            return this;
        },

        saveMovie: function (e) {
            e.preventDefault();

            var $this = this;

            this.model.set({
                'Title': $.trim($('#title-input').val()),
                'YearReleased': $.trim($('#yearreleased-input').val()),
                'Directors': $.trim($('#directors-input').val()),
                'Actors': $.trim($('#actors-input').val()),
                'Plot': $.trim($('#plot-input').val()),
                'Notes': $.trim($('#notes-input').val())
            });

		    $('.genre-checkbox:checked').each(function () {
		        var id = $(this).attr('data-genreId');
		        var g = $this.genres.get(id);

		        $this.model.get('Genres').add(g);  
		    });

		    this.model.on('invalid', function (m, errors, options) {
		        utils.showAlert("Save failed", "The validation failed", "alert-error");

		        for (var i = 0; i < errors.length; i++) {
		            var error = errors[i];
		            var field = error.name.toLowerCase() + '-input';

		            var controlGroup = $('#' + field).parent().parent();
		            controlGroup.addClass('error');

		            $('.help-inline', controlGroup).html(error.message);
		        }
		    });
            
		    this.model.save(null, {
		        success: function (model, res) {
		            console.log('saving');
		            if (res && res.errors) {
		            } else {
		                model.trigger('save-success', model.get('Id'));
		            }
		        },
		        error: function () {
		            console.log('error saving');
		            console.log(arguments);
		        }
		    });

		},

		cancel: function (e) {
			e.preventDefault();

			this.trigger('back');
		}
	});

	return MovieEditView;
});