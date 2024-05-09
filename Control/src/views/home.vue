<template>
  <view style="">
    <view style="">
      <img style="width: 240px" src="@/assets/logo.svg" />
    </view>
    <div>
      <div>交互与反馈</div>
      <div>
        <el-button type="info" v-on:click="wln_toast()"><el-icon><Pointer /></el-icon>wln.toast</el-button>
        <el-button type="info" v-on:click="wln_alert()"><el-icon><Pointer /></el-icon>wln.alert</el-button>
        <el-button type="info" v-on:click="wln_confirm"><el-icon><Pointer /></el-icon>wln.confirm</el-button>
      </div>
    </div>
    <div>
      <div>路由与页面跳转</div>
      <div>
          <el-button type="info" v-on:click="wln_gourl('https://www.baidu.com')"><el-icon><Pointer /></el-icon>wln.gourl</el-button>
          <el-button type="info" v-on:click="wln_goback()"><el-icon><Pointer /></el-icon>wln.goback</el-button>
          <el-button type="info" v-on:click="wln_login()"><el-icon><Pointer /></el-icon>wln.login</el-button>
          <el-button type="info" v-on:click="wln_logout()"><el-icon><Pointer /></el-icon>wln.logout</el-button>
      </div>
    </div>
    <div>
      <div>演示页面</div>
      <div>
          <el-button type="info" v-on:click="wln_gourl('/content/category_list')"><el-icon><Pointer /></el-icon>栏目管理</el-button>
      </div>
    </div>
  </view>
</template>

<script setup>
  import { ref, reactive, onMounted, getCurrentInstance } from 'vue'
  import { mQuery, mPager } from "@/wlnapp/model.js"
  const { proxy } = getCurrentInstance()
  const wln = proxy.$wln
  const query = reactive(mQuery)
  const pager = reactive(mPager)
  const activeName = ref('1')
  const tipmsg = '演示功能：'
  function getlist(size) {
    if(size) { query.page = parseInt(size) || 0 }
    wln.api('/service/admin/dossier_pager', (res) => {
      if(res.rows) {
        pager.rows = res.rows
        pager.total = res.total
        pager.message = res.message
      } else {
        pager.rows = []
        pager.message = res.message
      }
    }, query, true, true)
  }
  function modify() {
    console.log('modify')
  }
  function wln_toast() {
    wln.toast('一条默认反馈提示消息')
    wln.toast('一条表示成功的反馈提示消息', 'success')
    wln.toast('一条表示警告的反馈提示消息', 'warning')
    wln.toast('一条表示错误的反馈提示消息', 'error')
  }
  function wln_alert() {
    wln.alert('这是一个对话框消息？',() => { wln.toast('已确认并关闭对话框') })
  }
  function wln_confirm() {
    wln.confirm('这是一个危险操作，需要继续吗？',() => { wln.toast('选择了：继续') }, ()=>{ wln.toast('选择了：放弃') }, '继续', '放弃')
  }
  
  function wln_gourl(url) {
    wln.toast(tipmsg + '页面跳转')
    wln.gourl(url)
  }
  function wln_goback() {
    wln.toast(tipmsg + '返回上一页')
    wln.goback(1)
  }
  function wln_login() {
    wln.toast(tipmsg + '跳转到登录页面')
    wln.login()
  }
  function wln_logout() {
    wln.toast(tipmsg + '注销登录')
    wln.logout()
  }

  onMounted(() => {
    getlist(0)
    //wln.debug(`debug info`)
    //proxy.$wln.logout()
  })
</script>
