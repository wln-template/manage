<template>
    <div>
        <el-input clearable v-model="query.key" placeholder="Enter 进行筛选" style="width:270px" v-on:keydown.enter.native="getlist(0)"></el-input>
        <el-button type="info" v-on:click="getlist(0)"><el-icon><Refresh /></el-icon>重新加载</el-button>
        <el-button type="primary" v-on:click="modify('')"><el-icon><Plus /></el-icon>&nbsp;新商户号申请</el-button>
    </div>
    <div class="wln-line"></div>
    <el-table :data="pager.rows" :empty-text="pager.message" style="width:100%">
        <el-table-column width="80" align="center" label="状态" fixed="left">
            <template #default="scope">
                <div class="fs13 lh18">
                    <font style="color:#67C23A" v-if="scope.row.state===3">已生效</font>
                    <font style="color:#409eff" v-else-if="scope.row.state===2">待审核</font>
                    <font style="color:#c73b04" v-else-if="scope.row.state===1">待修改</font>
                    <font style="color:#909399" v-else>未提交</font>
                </div>
            </template>
        </el-table-column>
        <el-table-column width="320" label="商户名/商户号" fixed="left">
            <template #default="scope">
                <div class="fs13 lh18">{{scope.row.name}}</div>
                <div class="fs12 lh14 c99">{{scope.row.id}}</div>
            </template>
        </el-table-column>
        <el-table-column width="240" label="结算账户开户行/账号">
            <template #default="scope">
                <template v-if="scope.row.dossier">
                    <div class="fs13 lh18">{{scope.row.bank_name}}{{scope.row.bank_point}}</div>
                    <div class="fs12 lh14 c99">{{scope.row.bank_account}}</div>
                </template>
                <template v-else>
                    <div class="fs13 lh18">资料未审核</div>
                    <div class="fs12 lh14 c99">请耐心等待资料审核</div>
                </template>
            </template>
        </el-table-column>
        <el-table-column width="168" align="center" label="微信 ">
            <template #default="scope">
                <div class="fs13 lh18">
                    <font style="color:#67C23A" v-if="scope.row.state1===2">已开通</font>
                    <font style="color:#409eff" v-if="scope.row.state1===1">待校验</font>
                    <font style="color:#909399" v-else>未开通</font>
                </div>
                <div class="fs12 lh14 c99">{{scope.row.mchid1}}</div>
            </template>
        </el-table-column>
        <el-table-column width="168" align="center" label="支付宝">
            <template #default="scope">
                <div class="fs13 lh18">
                    <font style="color:#67C23A" v-if="scope.row.state2===2">已开通</font>
                    <font style="color:#409eff" v-if="scope.row.state2===1">待校验</font>
                    <font style="color:#909399" v-else>未开通</font>
                </div>
                <div class="fs12 lh14 c99">{{scope.row.mchid2}}</div>
            </template>
        </el-table-column>
        <el-table-column width="168" align="center" label="聚合支付">
            <template #default="scope">
                <div class="fs13 lh18">
                    <font style="color:#67C23A" v-if="scope.row.state3===2">{{scope.row.channel3name}}</font>
                    <font style="color:#409eff" v-else-if="scope.row.state3===1">{{scope.row.channel3name}}</font>
                    <font style="color:#909399" v-else>未开通</font>
                </div>
                <div class="fs12 lh14 c99">{{scope.row.mchid3}}</div>
            </template>
        </el-table-column>
        <el-table-column></el-table-column>
        <el-table-column width="160" label="提交人员/时间">
            <template #default="scope">
                <div v-if="scope.row.submit_time && scope.row.submit_user">
                    <div class="fs13 lh18">{{scope.row.submit_user}}</div>
                    <div class="fs13 lh14 c99"><font class="fs12">{{scope.row.submit_user}}</font></div>
                </div>
                <div class="fs13 lh14 c99" v-else>暂未提交</div>
            </template>
        </el-table-column>
        <el-table-column width="108" align="center" fixed="right">
            <template #default="scope">
                <el-dropdown>
                    <el-button type="primary">操作&nbsp;<el-icon><ArrowDownBold /></el-icon></el-button>
                    <el-dropdown-menu slot="dropdown">
                        <el-dropdown-item v-on:click.native="toview(scope.row)">查看详情</el-dropdown-item>
                        <el-dropdown-item v-on:click.native="toview(scope.row)">支付渠道管理</el-dropdown-item>
                    </el-dropdown-menu>
                </el-dropdown>
            </template>
        </el-table-column>
    </el-table>
    <el-pagination v-on:current-change="getlist" layout="total, prev, pager, next, jumper" :total="pager.total" :current-page="query.page" :page-size="query.size"></el-pagination>
</template>

<script setup>
    import { ref, reactive, onMounted, getCurrentInstance } from 'vue'
    import { mQuery, mPager } from "@/wlnapp/model.js"
    const { proxy } = getCurrentInstance()
    const wln = proxy.$wln
    const query = reactive(mQuery)
    const pager = reactive(mPager)
    function getlist(size) {
        if (size) { query.page = parseInt(size) || 0 }
        wln.api('/content/category_list', (res) => {
            if (res.rows) {
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
    onMounted(() => {
        getlist(0)
        //wln.debug(`debug info`)
        //proxy.$wln.logout()
    })
</script>