define([
	'backbone',
    'models/movie',
    'collections/genres',
	'text!templates/movie/edit.html'
], function (Backbone, Movie, GenreCollection, movieEditTemplate) {
    var acceptableTypes = ['image/jpeg'];

    var createCallback = function (limit, fn) {
        var finishedCalls = 0;

        return function () {
            if (++finishedCalls == limit) {
                fn();
            }
        };
    };

    $.event.props.push('dataTransfer');
    
    var MovieEditView = Backbone.View.extend({
        events: {
            'change': 'change',
            'click .save-btn': 'saveMovie',
            'click .back-btn': 'cancel',
            'dragover .image_upload_drop_zone': 'dragOver',
            'drop .image_upload_drop_zone': 'drop'
        },

        initialize: function () {
            this.template = _.template(movieEditTemplate);
            this.genres = new GenreCollection();

            this.model.on('invalid', function (m, errors, options) {
                utils.showAlert("Save failed", "The validation failed", "alert-error");

                for (var e in errors) {
                    utils.displayFieldError({ name: e, message: errors[e] });
                }
            });
        },

        change: function (event) {
            var target = event.target;
            
            if (target.id.indexOf('genre') != -1) {
                var genreId = $(target).attr('data-genreId');
                var genres = this.model.get('Genres');
                var genre = this.genres.get(genreId);

                if (target.checked) {
                    genres.add(genre);
                } else {
                    genres.remove(genre);
                }
            } else {
                var id = target.id.substring(0, target.id.indexOf('-'));
                var change = {};
                change[id] = target.value;

                this.model.set(change);

                var check = this.model.validateItem(id);
                if (check.isValid === false) {
                    utils.displayFieldError({
                        name: id,
                        message: check.message
                    });
                } else {
                    utils.removeFieldError(id);
                }
            }
        },

        dragOver: function (e) {
            e.preventDefault();
            e.stopPropagation();
        },

        drop: function (e) {
            e.preventDefault();
            e.stopPropagation();

            this.fileToUpload = e.dataTransfer.files[0];

            if (acceptableTypes.indexOf(this.fileToUpload.type) != null) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    $('#posterDroppable').hide();
                    $('#posterDroppable').attr('src', e.target.result);
                    $('#posterDroppable').fadeIn('slow');
                };
                reader.readAsDataURL(this.fileToUpload);
            }
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
                $this.$el.html($this.template({
                    movie: $this.model.toJSON(),
                    genres: $this.genres.toJSON(),
                    isNew: $this.model.isNew()
                }));
            });
            
            this.loadGenres(cb);
            this.loadMovie(cb);

            return this;
        },

        saveMovie: function (e) {
            e.preventDefault();
            $this = this;

            if (this.fileToUpload) {
                utils.uploadPoster(this.fileToUpload, function (posterFilename) {
                    console.log(posterFilename);
                    $this.model.set('PosterFilename', posterFilename);
                    $this.newSaveMovie();
                });
            } else {
                $this.newSaveMovie();
            }
        },

        newSaveMovie: function () {
            var $this = this;

            this.model.save(null, {
                success: function (model, res) {
                    if (res && res.errors) {
                    } else {
                        var movieId = model.get('Id');
                        model.trigger('save-success', movieId);
                    }
                }
            });
        },

        savePoster: function(movieId, cb) {
        },

		cancel: function (e) {
			e.preventDefault();

			this.trigger('back');
		}
	});

	return MovieEditView;
});