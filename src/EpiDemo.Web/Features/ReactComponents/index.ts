import * as React from "react";
import * as ReactDOM from "react-dom";
import * as ReactDOMServer from "react-dom/server";
import { Counter } from "../Partials/Counter/Counter";

declare var global: any;

const Components:any = {
    "Counter": Counter,
};

// register components as global required by ReactJS.NET
for(const component in Components) {
    global[component] = Components[component];
}

global.React = React;
global.ReactDOM = ReactDOM;
global.ReactDOMServer = ReactDOMServer;