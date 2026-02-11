pipeline {
    agent any

    environment {
        // .NET SDK
        DOTNET_ROOT = 'C:\\Program Files\\dotnet'

        // Solution details
        SOLUTION_NAME = 'practise1.sln'

        // Project details
        PROJECT_PATH = 'src\\practise1'
        PROJECT_NAME = 'practise1.csproj'
    }

    stages {
        stage('Build') {
            steps {
                echo 'Building application...'
                bat 'dotnet build'
            }
        }
        			        stage('SonarQube Analysis') {
			            steps {
			                script {
			                    // Assign tool inside script block
			                    def scannerHome = tool 'SonarScanner for MSBuild'
			                    // Use withSonarQubeEnv inside script block
			                    withSonarQubeEnv('MySonarQube') {
			                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" begin /k:\"Batch33\""
			                        bat "dotnet build"
			                        bat "\"${scannerHome}\\SonarScanner.MSBuild.exe\" end"
			                    }
			                }
			            }
			        }

        stage('Test') {
            steps {
                echo 'Running tests...'
                bat 'dotnet test'
            }
        }

        stage('Deploy') {
            steps {
                echo 'Deploying application...'
                bat 'dotnet publish -c Release -o publish'
            }
        }
    }

    post {
        success {
            echo 'Pipeline completed successfully'
        }
        failure {
            echo 'Pipeline failed'
        }
    }
}
