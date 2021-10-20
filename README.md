# Unity Starter Project

Template to start new Unity's VR projects.

[![CodeFactor](https://www.codefactor.io/repository/github/innerspacetrainings/unity-template/badge?s=f0279fd7c65da74cd87c263b892350aa11cdaf46&style=for-the-badge)](https://www.codefactor.io/repository/github/innerspacetrainings/unity-template)

## Cloning

Remember to enable `recursive-submodules` when cloning

## Instructions

##### Instructions to follow after you created the Repository

- Create two branches, one called develop and the other one called master. 
- Set develop as the [default branch](https://help.github.com/en/articles/setting-the-default-branch) in settings. 
- Subscribe the slack channel `#dev-unity` to new Github events: `/github subscribe innerspacetrainings/RepoName pulls commits`
- Create a [Unity Service Project](https://unity3d.com/fr/learn/tutorials/topics/cloud-build/your-first-cloud-build-project) for the new project and **link it in the editor**. 
- Add slack as an integration service to successful or failed builds in the integrations page in your Unity Service settings page of your project. 
- Modify the `ProjectConfiguration` file to work with the new ID.
- Create a [CodeFactor project](https://www.codefactor.io/dashboard) for the new Repository. 
- Set the [CodeFactor exclude patterns](#codefactor-exclude-patterns) for your new repository.
- Modify the [Prace configuration file](https://github.com/innerspacetrainings/prace.js#repository-configuration-file) to have the correct regex. 
- Modify the [Craud configuration file](https://github.com/innerspacetrainings/CRAUD#client-instalation) to have the correct project ID. 
- Add Craud as a [Webhook](https://developer.cloud.unity3d.com/orgs/innerspace-trainings/projects/tksingen/integrations/subscription/d65d847c-c45a-4f6b-9ea6-8906304d4111/) interaction to the newly created project in the unity service project. 
- Replace this readme with some useful information and the [CodeFactor badge](https://support.codefactor.io/i32-using-branch-badges). 
- Do a pull request with all the changes to develop and wait for all the status check to pass (CodeFactor, Craud and Prace). 
- Merge and then [enable branch protection](#how-to-set-branch-protection). 
- Finally, create two configs in *Unity Cloud Build*, one for develop and one for master. Both of them should have **auto build** turned on.

### How to set branch protection
Set this for both develop and master branch

- [x] Require pull request reviews before merging (at least one reviewer) 
    - [x] Require review from Code Owners 
- [x] Require status checks to pass before merging 
    - [x] Require branches to be up to date before merging 
        - [x] Pull Request Automated Convention Enforcer 
        - [x] Unity Cloud Build PR CI 
- [x] Restrict who can push to matching branches (set this true for _master_) 
    - [ ] Keep field empty

### CodeFactor exclude patterns

Add the following directories to your excluded patterns in your settings

```
Assets/Plugins/**
Assets/Standard Assets/**
Assets/_Projects/Toolkit/**
```