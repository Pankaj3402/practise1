pipeline {
    agent any

    environment {
        // .NET SDK
        DOTNET_ROOT   = 'C:\\Program Files\\dotnet'

        // Solution details
        SOLUTION_NAME = 'practise1.sln'

        // Project details
        PROJECT_PATH = 'src\\practise1'
        PROJECT_NAME = 'practise1.csproj'

    stages {
        stage('Build') {
            steps {
                echo 'Building application...'
                bat 'dotnet build'
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
