<template>
    <div class="main-container" id="viewerContainer">
    </div>
</template>
<script>

    import {createViewer} from '@mescius/activereportsnet-viewer-ja';
    import "@mescius/activereportsnet-viewer-ja/esm/jsViewer.min.css";
    export default {
        props: ['reportName'],
        mounted() {
            this.viewer = createViewer({
                element: '#viewerContainer'
            });
        },
        watch: {
            reportName() {
                this.viewer.openReport(this.reportName);
            }
        },
    }
</script>
<style>
    .main-container {
      height: 100vh;
      flex: 3;
    }
</style>