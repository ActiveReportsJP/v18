<template>
  <div class="sidebar-container">
    <div class="sidebar-header">レポートの選択</div>
    <div class="horizontal-separator"></div>
    <ul class="navbar" id="reportsList">
      <li class="navbar-item"
          v-bind:class="{active: selectedReport === name}"
          v-for="(name, index) in reportList"
          v-on:click="selectReport(name)">
        <span>{{ name }}</span>
      </li>
    </ul>
  </div>
</template>
<script>
export default {
  props: ['reportList', 'selectedReport', 'selectReport'],
}
</script>
<style>
    .sidebar-container {
      background: #F1F1F1;
      color: #333;
      width: 200pt;
      display: flex;
      flex-direction: column;
    }

    #reportsList {
        height: calc(100vh - 51px - 3rem);
        width: 100%;
        overflow: auto;
        flex-wrap: nowrap;
    }
    
    .sidebar-header {
      padding: 16px;
      text-align: center;
    }
    
    .horizontal-separator {
      height: 1px;
      background-color: rgba(0, 0, 0, 0.11);
      color: #0c5f78;
      margin: 0 10px;
    }
    
    .navbar {
      padding: .5rem 0;
      overflow: auto;
    }
    
    .navbar-item {
      cursor: pointer;
      color: #2b2b2b;
      padding: 0 1rem;
      font-size: 15px;
      height: 34px;
      display: flex;
      align-items: center;
      transition-duration: .2s;
    }
    
    .navbar-item > span {
      white-space: nowrap;
      overflow: hidden;
      text-overflow: ellipsis;
    }
    
    .navbar-item:hover, .navbar-item.active:hover {
      color: white;
      background-color: #1b5166;
    }
    
    .navbar-item.active {
      color: white;
      background-color: #3b7186;
    }
</style>