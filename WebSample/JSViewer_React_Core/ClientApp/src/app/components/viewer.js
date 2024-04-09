import React, {Component} from "react";
import {createViewer} from '@mescius/activereportsnet-viewer-ja';

import "@mescius/activereportsnet-viewer-ja/dist/jsViewer.min.css";

export default class Viewer extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        this.viewer = createViewer({
            element: '#viewerContainer'
        });
    }

    componentDidUpdate() {
        this.viewer.openReport(this.props.reportName);
    }

    render() {
        return (
            <div className="main-container" id="viewerContainer"/>
        );
    }
}
