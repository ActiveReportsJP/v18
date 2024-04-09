import * as React from "react";
import {reportContext} from "./reportProvider";

export const ReportsList: React.FC = () => {
    const reportProvider = React.useContext(reportContext);

    return (
        <div className="sidebar-container">
            <div className="sidebar-header">レポートを選択</div>
            <div className="horizontal-separator"></div>
            <ul className="navbar" id="reportsList">
                {reportProvider?.reportsList.map((report) => (
                    <li
                        className={
                            "navbar-item" +
                            (report === reportProvider.currentReport ? " active" : "")
                        }
                        key={report}
                        onClick={() => {
                            reportProvider?.setCurrentReport(report);
                        }}
                    >
                        <span>{report}</span>
                    </li>
                ))}
            </ul>
        </div>
    );
};
