const path = require('path');
const webpack = require('webpack');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const AssetsPlugin = require('assets-webpack-plugin');
const WebpackMd5Hash = require('webpack-md5-hash');
const glob = require('glob');
const WebpackPwaManifest = require('webpack-pwa-manifest');

const featureAssets = glob.sync('./Features/**/*.@(ts|css)');
const globalAssets = glob.sync('./Static/+(Styles|Images|Icons|Scripts|Animations)/**/*.+(ico|png|css|svg|ts)');

const TerserJSPlugin = require("terser-webpack-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");

module.exports = function (env, argv) {

    const serverBuild = env && env.serverBuild;
    const devMode = argv.mode !== "production";
    const publicPath = '/dist';
    const outputPath = path.resolve(__dirname, 'dist');

    const plugins = [];
    plugins.push(new WebpackMd5Hash());

    plugins.push(new AssetsPlugin({
        path: outputPath,
	    prettyPrint: true,
	    update: true
    }));

    if (!serverBuild && !devMode) {
        plugins.push(new MiniCssExtractPlugin({
            filename: '[name].[chunkhash].css',
            chunkFilename: '[id].[chunkhash].css'
        }));
    }

    if (devMode) {
        plugins.push(new webpack.HotModuleReplacementPlugin());
    }

    return {
        optimization: {
            minimizer: [
                new TerserJSPlugin({}),
                new OptimizeCSSAssetsPlugin({})
            ]
        },
        entry: serverBuild
            ? {
                "server-side": "./Features/ReactComponents/index.ts"
            }
            : {
                "features": globalAssets.concat(featureAssets),
            },
        output: {
            path: outputPath,
            filename: devMode ? '[name].js':'[name].[chunkhash].js',
            publicPath: publicPath
        },
        devtool: 'source-map',
        resolve: {
            extensions: ['.js', '.jsx', '.json', '.ts', '.tsx']
        },
        module: {
            rules: [
                {
                    test: /\.css$/,
                    use: [
                        devMode ? 'style-loader' : MiniCssExtractPlugin.loader,
                        'css-loader',
                        'postcss-loader'
                    ]
                },
                {
                    test: /\.(woff|woff2|eot|ttf|otf)$/,
                    use: {
                        loader: 'file-loader',
                        options: {
                            name: '[name].[hash].[ext]', /*  [contenthash] not supported by file-loader */
                            publicPath: publicPath
                        }
                    }
                },
                {
                    test: /\.(ts|tsx)$/,
                    use: {
                        loader: 'ts-loader',
                        options: {
                            // transpileOnly: true,
                            // experimentalWatchApi: true  
                        }
                    }
                },
                {
                    test: /\.(ico|png|svg)$/,
                    use: {
                        loader: 'file-loader',
                        options: {
                            name: 'assets/images/[name].[ext]', 
                            publicPath: publicPath
                        }
                    }
                },
                {
                    enforce: "pre", test: /\.(ts|tsx|js|jsx)$/,
                    loader: "source-map-loader"
                }
            ]
        },
        plugins: plugins,

        // optimization: {
        //     splitChunks: {
        //         chunks: "all",
        //         minSize: 0
        //     }
        // },
        devServer: {}
    }
};
