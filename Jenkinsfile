pipeline {
    agent any

    environment {
        SONARQUBE_SERVER = 'SonarQubeServer'  // Configure in Jenkins > Global Tool Config
        DOCKER_CREDENTIALS = credentials('dockerhub-creds') // DockerHub Credentials
        IMAGE_NAME = 'ghanshyamnimbalkar/aspnetmvcapp'
        BRANCH_NAME = "${env.GIT_BRANCH}".replaceAll("/", "-")
        BUILD_TAG = "${IMAGE_NAME}:${BRANCH_NAME}-${env.BUILD_NUMBER}"
    }

    triggers {
        githubPush() // or use pollSCM('H/2 * * * *')
    }

    stages {
        echo "Continuous Integration Started Successfully!!!!!!!"

        stage('Checkout') {
            steps {
                echo "Stage Checkout started!!!!!"
                git url: 'https://github.com/yourname/your-repo.git', branch: 'master'
                echo "Stage Checkout finished successfully!!!!!"
            }
        }

        stage('Restore & Build') {
            steps {
                echo "Stage Restore & Build started...."
                
                sh 'dotnet restore'
                sh 'dotnet build --configuration Release'

               echo "Stage Restore & Build finished successfully!!!!!"
            }
        }

        stage('Run Tests') {
            steps {
                echo "Stage Tests started...."
                sh 'dotnet test --no-build --verbosity normal'
                echo "Stage Tests executed successfully!!!!!"
            }
        }

        stage('SonarQube Analysis') {
            steps {
                echo "Static code ananlysis using SonarQube started...."
                withSonarQubeEnv('SonarQubeServer') {
                    sh 'dotnet sonarscanner begin /k:"aspnetmvc" /d:sonar.login="$SONAR_TOKEN"'
                    sh 'dotnet build'
                    sh 'dotnet sonarscanner end /d:sonar.login="$SONAR_TOKEN"'
                    echo "Static code ananlysis done successfully!!!!!"
                }
            }
        }

        stage('Build Docker Image') {
            steps {
                echo "Building docker image has been started...."
                sh 'docker build -t $BUILD_TAG .'
                echo "Doker image has been build successfully!!!!!"
            }
        }

        stage('Push to DockerHub or ECR') {
            echo "pushing docker image to the dockerhub process has been started...."
            steps {
                script {
                    docker.withRegistry('https://index.docker.io/v1/', 'dockerhub-creds') {
                        sh "docker push $BUILD_TAG"

                        echo "Doker image has been pushed to dockerhub successfully!!!!!"
                    }
                }
            }
        }

        stage('Archive Build (Optional)') {
            steps {
                archiveArtifacts artifacts: '**/bin/**/*.dll', fingerprint: true
            }
        }
    }

    post {
        success {
            echo "CI pipeline successful. Image pushed: $BUILD_TAG"
        }
        failure {
            echo "Pipeline failed!"
        }
    }
}
