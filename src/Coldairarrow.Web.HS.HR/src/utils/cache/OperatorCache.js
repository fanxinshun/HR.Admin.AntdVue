import {
  Axios
} from "@/utils/plugin/axios-plugin"

import TokenCache from '@/utils/cache/TokenCache'

let permissions = []
let inited = false

let OperatorCache = {
  info: {},
  projectList: [],
  inited() {
    return inited
  },
  init(callBack) {
    if (inited)
      callBack()
    else {
      Axios.post('/Base_Manage/Home/GetOperatorInfo').then(resJson => {
        if (resJson.Success) {
          this.info = resJson.Data.UserInfo
          this.projectList = resJson.Data.ProjectList
          permissions = resJson.Data.Permissions
          inited = true
          callBack()
        }
      })
    }
  },
  hasPermission(thePermission) {
    return permissions.includes(thePermission)
  },
  clear() {
    inited = false
    permissions = []
    this.info = {}
  },
  changeProject(p) {
    this.loading = true
    Axios.post('/Base_Manage/Home/UpdateUserLastInterviewProject', {
      "projectId": p,
      "token": TokenCache.getToken()
    }).then(resJson => {
      this.loading = false
      if (resJson.Success) {
        TokenCache.setProject(p)
        TokenCache.setToken(resJson.Data)
        location.reload()
      } else {
        TokenCache.deleteToken()
        this.clear()
        location.reload()
      }
    })
  }
}

export default OperatorCache
