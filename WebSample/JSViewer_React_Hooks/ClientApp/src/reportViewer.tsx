import React, { useEffect, useRef } from 'react';

import { JSViewer, createViewer} from '@mescius/activereportsnet-viewer-ja';
import "@mescius/activereportsnet-viewer-ja/esm/jsViewer.min.css";

type ViewerProps = {
  reportId: string
};

export function ReportViewer({reportId}: ViewerProps) {
  const viewerRef = useRef<JSViewer | undefined>(undefined);

  useEffect(() => {
    if(viewerRef.current == undefined){
      viewerRef.current = createViewer({
        element: "#viewerContainer",
      });
    }
  }, []);

  useEffect(() => {
    if (reportId && viewerRef.current) {
      viewerRef.current.openReport(reportId);
    }
  }, [reportId]);

  return <div className="main-container" id="viewerContainer" />;
}
