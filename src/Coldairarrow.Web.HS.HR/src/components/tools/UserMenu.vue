<template>
  <div class="user-wrapper">
    <div class="content-box">
      <!-- <span>项目地:</span> -->
      <!-- <a href="https://pro.loacg.com/docs/getting-started" target="_blank">
        <span class="action">
          <a-icon type="question-circle-o"></a-icon>
        </span>
      </a> -->
      <!-- <notice-icon class="action" /> -->
      <a-dropdown>
        <span class="action ant-dropdown-link user-dropdown-menu">
          <a-avatar size="small" icon="appstore" />
          <span>项目地：{{ UserInfo.Project.projectName }}</span>
        </span>
        <a-menu slot="overlay" class="user-dropdown-menu-wrapper">
          <a-menu-item v-for="item in ProjectList" :key="item.Id" @click="projectSelected(item.Id)">
            <span>{{ item.Project_Name }}</span>
          </a-menu-item>
        </a-menu>
      </a-dropdown>
      <a-dropdown>
        <span class="action ant-dropdown-link user-dropdown-menu">
          <a-avatar size="small" icon="user" />
          <span>欢迎：{{ UserInfo.UserName }}</span>
        </span>
        <a-menu slot="overlay" class="user-dropdown-menu-wrapper">
          <a-menu-item key="1">
            <a href="javascript:;" @click="handleChangePwd()">
              <a-icon type="lock" />
              <span>修改密码</span>
            </a>
            <change-pwd-form ref="changePwd"></change-pwd-form>
          </a-menu-item>
          <a-menu-divider />
          <a-menu-item key="3">
            <a href="javascript:;" @click="handleLogout()">
              <a-icon type="logout" />
              <span>退出登录</span>
            </a>
          </a-menu-item>
        </a-menu>
      </a-dropdown>
    </div>
  </div>
</template>

<script>
  // import NoticeIcon from '@/components/NoticeIcon'
  // import { mapActions, mapGetters } from 'vuex'
  import OperatorCache from '@/utils/cache/OperatorCache'
  import TokenCache from '@/utils/cache/TokenCache'
  import ChangePwdForm from './ChangePwdForm'

  export default {
    name: 'UserMenu',
    components: {
      // NoticeIcon
      ChangePwdForm
    },
    mounted() {
      this.op()
    },
    data() {
      return {
        ProjectList: [],
        UserInfo: {
          UserName: '',
          Project: {
            projectName: '',
            projectId: '',
            projectCode: ''
          }
        }
      }
    },
    methods: {
      op() {
        this.UserInfo.UserName = OperatorCache.info.UserName
        //用户项目列表
        this.ProjectList = OperatorCache.projectList
        let projectId = OperatorCache.info.Last_Interview_Project
        //当前项目信息
        let Project = this.ProjectList.find(x => x.Id === TokenCache.getProject()) || this.ProjectList.find(
          x => x.Id === projectId)
        if (Project) {
          TokenCache.setProject(Project.Id)
          this.UserInfo.Project.projectName = Project.Project_Name
          this.UserInfo.Project.projectCode = Project.Project_Code
          this.UserInfo.Project.projectId = Project.Id
        }
      },
      // ...mapActions(['Logout']),
      // ...mapGetters(['nickname', 'avatar']),
      handleLogout() {
        const that = this
        this.$confirm({
          title: '提示',
          content: '确认要注销登录吗 ?',
          onOk() {
            TokenCache.deleteToken()
            OperatorCache.clear()
            location.reload()
            // that.$router.push({ path: '/user/login' })
          }
        })
      },
      handleChangePwd() {
        this.$refs.changePwd.open()
      },
      //切换项目
      projectSelected(p) {
        if (this.UserInfo.Project.projectId == p) return
        OperatorCache.changeProject(p)
      }
    }
  }
</script>
