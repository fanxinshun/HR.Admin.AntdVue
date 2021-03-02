const tokenKey = 'hr_jwtToken'
const projectKey = 'hr_project_id'

let TokenCache = {
  getToken() {
    return localStorage.getItem(tokenKey)
  },
  setToken(newToken) {
    localStorage.setItem(tokenKey, newToken)
  },
  deleteToken() {
    localStorage.removeItem(tokenKey)
  },
  getProject() {
    return localStorage.getItem(projectKey)
  },
  setProject(newproject) {
    localStorage.setItem(projectKey, newproject)
  }
}

export default TokenCache
