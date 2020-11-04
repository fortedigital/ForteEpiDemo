module.exports = {
    plugins: {
        "postcss-normalize": true,
        "postcss-mixins": {
            "mixinsDir": "Static/Styles/mixins"
        },
        "postcss-custom-media": {
            "preserve": false,
            "importFrom": "Static/Styles/media.css"
        },
        "postcss-custom-properties": {
            "preserve": false,
            "importFrom": "Static/Styles/variables.css",
        },
        "postcss-color-function" : true,
        "postcss-inline-svg": {
            "path": "Static/Images/"
        },
        "postcss-nesting": true,
        "postcss-svgo": true,
        "lost": true,
        "autoprefixer": true,
    }
};
