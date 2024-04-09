<template>
<div class="page-container">
    <ReportList 
        :reportList="reportList" 
        :selectedReport="selectedReport" 
        :selectReport="selectReport"/>
    <Viewer :reportName="selectedReport"/>
</div>
</template>
<script>
import ReportList from './components/ReportList.vue';
import Viewer from './components/Viewer.vue';

export default {
    name: 'app',
    components: {
        ReportList, Viewer
    },
    data() {
        return {
            reportList: [],
            selectedReport: ""
        };
    },
    beforeCreate() {
        fetch('reports')
            .then(response => response.json())
            .then(result => {
                if(result.length > 0){
                    this.selectedReport = result[0];
                    this.reportList = result;
                }
            })
            .catch(e => {
                console.log(e);
            });
    },
    methods: {
        selectReport(reportName) {
            this.selectedReport = reportName;
        }
    }
}
</script>
<style>
    .page-container {
      display: flex;
    }
</style>